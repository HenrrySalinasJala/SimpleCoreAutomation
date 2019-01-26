namespace Automation.Simple.Core.UI.Actions.Control
{
    using Automation.Simple.Core.Environment;
    using Automation.Simple.Core.UI.Actions.Enums;
    using log4net;
    using System;

    public class LoginActions
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection
                                                                        .MethodBase
                                                                        .GetCurrentMethod()
                                                                        .DeclaringType);
        public bool LoginAs()
        {
            var currentTimeSpan = Config.ExplicitTimeoutInSeconds;
            try
            {
                Config.ExplicitTimeoutInSeconds = 10;
                var user = Config.UserForWebApp;
                var password = Config.PasswordForWebApp;
                var timeoutInSeconds = TimeSpan.FromSeconds(10);
                var controlAction = new ControlActions();

                bool isLoggedAs = false;
                bool? isSystemUserLogged = controlAction.ExecuteFunction("system_user", ActionType.IsDisplayed, string.Empty) as bool?;
                if ((bool)isSystemUserLogged)
                {
                    log.Info($"User already logged in...");
                    return !isLoggedAs;
                }
                else
                {
                    bool isUserSet = controlAction.Execute("system_user_login", ActionType.SetText, string.Empty, user);
                    bool isPassSet = controlAction.Execute("system_password_login", ActionType.SetText, string.Empty, password);
                    bool isSignedClicked = controlAction.Execute("system_sign_in_login", ActionType.Click, string.Empty);
                    return isUserSet & isPassSet & isSignedClicked;
                }
            }
            catch (Exception error)
            {
                log.Error($"Unable to log in {error.Message}");
                return false;
            }
            finally
            {
                Config.ExplicitTimeoutInSeconds = currentTimeSpan;
            }
        }
    }
}
