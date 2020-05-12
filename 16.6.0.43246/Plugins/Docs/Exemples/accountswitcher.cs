using System;
using SmartBot.Plugins.API;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SmartBot.Plugins
{
    [Serializable]
    public class AccountSwitcherDatas : PluginDataContainer
    {
        public AccountSwitcherDatas()
        {
            Name = "AccountSwitcher";
            SwitchOnDailyGoldCapReached = true;
        }

        public bool SwitchOnDailyGoldCapReached { get; set; }
        public bool StopBotWhenAllCapsReached { get; set; }

        [ItemsSource(typeof (ProfileStringSource))]
        public string MainAccountProfile { get; set; }

        [ItemsSource(typeof (MulliganStringSource))]
        public string MainAccountMulligan { get; set; }

        public string MainAccountDeck { get; set; }
        public string MainAccountLogin { get; set; }
        public string MainAccountPassword { get; set; }

        public Bot.Mode MainAccountMode { get; set; }

        [ItemsSource(typeof (ProfileStringSource))]
        public string SecondAccountProfile { get; set; }

        [ItemsSource(typeof (MulliganStringSource))]
        public string SecondAccountMulligan { get; set; }

        public string SecondAccountDeck { get; set; }
        public string SecondAccountLogin { get; set; }
        public string SecondAccountPassword { get; set; }

        public Bot.Mode SecondAccountMode { get; set; }
    }


    public class Account
    {
        public bool CapReached;
        public string Deck;
        public string Login;
        public Bot.Mode Mode;
        public string Mulligan;
        public string Password;
        public string Profile;

        public Account(string login, string password, string deck, string profile, string mulligan, Bot.Mode mode)
        {
            Login = login;
            Password = password;
            Deck = deck;
            Profile = profile;
            Mulligan = mulligan;
            Mode = mode;
            CapReached = false;
        }
    }

    public class bAccountSwitcher : Plugin
    {
        private Account MainAccount;
        private Account SecondAccount;

        private void SaveAccountsFromSettings()
        {
            Bot.Log("Account switcher - Saving settings");
            var dataContainer = (AccountSwitcherDatas) DataContainer;

            MainAccount = new Account(dataContainer.MainAccountLogin, dataContainer.MainAccountPassword,
                dataContainer.MainAccountDeck, dataContainer.MainAccountProfile, dataContainer.MainAccountMulligan,
                dataContainer.MainAccountMode);
            SecondAccount = new Account(dataContainer.SecondAccountLogin, dataContainer.SecondAccountPassword,
                dataContainer.SecondAccountDeck, dataContainer.SecondAccountProfile, dataContainer.SecondAccountMulligan,
                dataContainer.SecondAccountMode);
        }

        private void SwitchAccount()
        {
            if (!IsUsingMainAccount())
            {
                if (IsMainAccountValid())
                {
                    SwitchToMainAccount();
                }
            }
            else
            {
                if (IsSecondAccountValid())
                {
                    SwitchToSecondAccount();
                }
            }
        }

        private void SwitchToMainAccount()
        {
            SwitchSettings();
            Bot.SwitchAccount(MainAccount.Login, MainAccount.Password);
        }

        private void SwitchToSecondAccount()
        {
            SwitchSettings();
            Bot.SwitchAccount(SecondAccount.Login, SecondAccount.Password);
        }

        private bool IsUsingMainAccount()
        {
            if (IsMainAccountValid() == false) return false;
            return Bot.GetCurrentAccount() == MainAccount.Login;
        }

        private bool IsMainAccountValid()
        {
            if (MainAccount == null) return false;
            if (string.IsNullOrEmpty(MainAccount.Login)) return false;
            if (string.IsNullOrEmpty(MainAccount.Password)) return false;
            if (string.IsNullOrEmpty(MainAccount.Deck)) return false;

            return true;
        }

        private bool IsSecondAccountValid()
        {
            if (SecondAccount == null) return false;
            if (string.IsNullOrEmpty(SecondAccount.Login)) return false;
            if (string.IsNullOrEmpty(SecondAccount.Password)) return false;
            if (string.IsNullOrEmpty(SecondAccount.Deck)) return false;

            return true;
        }

        private void SwitchSettings()
        {
            Bot.ChangeProfile(!IsUsingMainAccount() ? MainAccount.Profile : SecondAccount.Profile);
            Bot.ChangeMulligan(!IsUsingMainAccount() ? MainAccount.Mulligan : SecondAccount.Mulligan);
            Bot.ChangeMode(!IsUsingMainAccount() ? MainAccount.Mode : SecondAccount.Mode);
        }

        private void SetGoldCapReached(Account acc)
        {
            acc.CapReached = true;
        }

        private bool IsGoldCapReachedOnAllAccounts()
        {
            return MainAccount.CapReached && SecondAccount.CapReached;
        }

        public override void OnStarted()
        {
            SaveAccountsFromSettings();
        }

        public override void OnGameEnd()
        {
            if (((AccountSwitcherDatas) DataContainer).StopBotWhenAllCapsReached && IsGoldCapReachedOnAllAccounts())
            {
                Bot.Log("Account switcher - Daily gold cap reached on all accounts, stopping bot");
                Bot.StopBot();
                Bot.StopRelogger();
                Bot.CloseHs();
                return;
            }

            if (Bot.IsGoldCapReached() && ((AccountSwitcherDatas) DataContainer).SwitchOnDailyGoldCapReached)
            {
                Bot.Log("Account switcher - Daily gold cap reached : switching to " +
                        (IsUsingMainAccount() ? "second account" : "main account"));

                (IsUsingMainAccount() ? MainAccount : SecondAccount).CapReached = true;
                SwitchAccount();
            }
        }

        public override void OnDecklistUpdate()
        {
            if (Bot.IsBotRunning())
            {
                Bot.ChangeDeck(IsUsingMainAccount() ? MainAccount.Deck : SecondAccount.Deck);
                Bot.Log("Account switcher - changing deck : " +
                        (IsUsingMainAccount() ? MainAccount.Deck : SecondAccount.Deck));
            }
        }
    }
}