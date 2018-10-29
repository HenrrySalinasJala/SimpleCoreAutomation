namespace Automation.Simple.Core.UI.Actions.Control
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using Automation.Simple.Core.UI.Actions.Exceptions;
    using Automation.Simple.Core.UI.Controls;
    using Automation.Simple.Helpers.Reflection;
    using System;

    public class ControlAction
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
            catch (NullReferenceException)
            {
                new ControlActionException($"Control '{controlName}' was not found or method '{actionType}' does not exist.");
                return false;
            }
            catch (ArgumentException)
            {
                new ControlActionException($"Method '{actionType}' for control '{controlName}' is non-void return type.");
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
            catch (NullReferenceException)
            {
                throw new ControlActionException($"Control '{controlName}' was not found or method '{actionType}' does not exist.");
            }
            catch (ArgumentException)
            {
                throw new ControlActionException($"Method '{actionType}' for control '{controlName}' is void return type.");
            }
        }
    }
}
