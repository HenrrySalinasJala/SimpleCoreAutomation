namespace Automation.Simple.Core.UI.Actions.Control
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using Automation.Simple.Core.UI.Actions.Exceptions;
    using Automation.Simple.Core.UI.Controls;
    using Automation.Simple.Core.UI.Exceptions;
    using Automation.Simple.Helpers.Reflection;
    using System;

    public class ControlActions
    {
        /// <summary>
        /// Lazy initialization for ControlFinder.
        /// </summary>
        private readonly Lazy<ControlFinder> _controlFinder = new Lazy<ControlFinder>();

        public bool Execute(string controlName, ActionType actionType, string frame,
            params object[] actionParams)
        {
            try
            {
                var control = _controlFinder.Value.Find(controlName, frame);
                var method = control.GetType().InstanceVoidMethod(actionType.ToString(), Type.GetTypeArray(actionParams));
                method(control, actionParams);
                return true;
            }
            catch (ControlNotFoundException error)
            {
                throw new ControlActionException($"Unable to find control {controlName} " +
                    $"with action {actionType} {error.Message}");
            }
            catch (NullReferenceException)
            {
                new ControlActionException($"method '{actionType}' does not exist for {controlName}.");
                return false;
            }
            catch (ArgumentException)
            {
                new ControlActionException($"Method '{actionType}' for control '{controlName}' is non-void return type.");
                return false;
            }
            catch (ControlActionExecutionException)
            {
                new ControlActionException($"Method '{actionType}' for control '{controlName}' is having issues executing.");
                return false;
            }
        }

        public object ExecuteFunction(string controlName, ActionType actionType, string frame,
            params object[] actionParams)
        {
            try
            {
                var control = _controlFinder.Value.Find(controlName, frame);
                var method = control.GetType().InstanceNonVoidMethod(actionType.ToString(), Type.GetTypeArray(actionParams));
                return method(control, actionParams);
            }
            catch (ControlNotFoundException error)
            {
                throw new ControlActionException($"Unable to find control {controlName} " +
                    $"with action {actionType} {error.Message}");
            }
            catch (NullReferenceException)
            {
                throw new ControlActionException($"method '{actionType}' does not exist for {controlName}.");
            }
            catch (ArgumentException)
            {
                throw new ControlActionException($"Method '{actionType}' for control '{controlName}' is void return type.");
            }
            catch (ControlActionExecutionException)
            {
                throw new ControlActionException($"Method '{actionType}' for control '{controlName}' is having issues executing.");
            }
        }
    }
}
