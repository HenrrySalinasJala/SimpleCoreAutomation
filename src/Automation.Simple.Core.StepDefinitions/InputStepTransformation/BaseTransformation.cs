using Automation.Simple.Core.StepDefinitions.DataTransformationTypes;
using Automation.Simple.Core.StepDefinitions.SpecFlow;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Automation.Simple.Core.StepDefinitions.InputStepTransformation
{
    [Binding]
    public class BaseTransformation
    {
        [StepArgumentTransformation]
        protected IStepArgument InputStringTransform(string stepArgument)
        {
            return new StringArgument(stepArgument);
        }

        /// <summary>
        /// Transforms the data table values to list of dictionaries.
        /// </summary>
        /// <param name="stepArgument">
        /// The step argument
        /// </param>
        /// <returns>
        /// returns List&lt;Dictionary&lt;string, string&gt;&gt;
        /// </returns>
        [StepArgumentTransformation]
        protected List<Dictionary<string, string>> InputTableTransform(object stepArgument)
        {
            var stepArgumentType = stepArgument.GetType();
            List<Dictionary<string, string>> dataTable;
            if (stepArgumentType == typeof(Table))
            {
                 dataTable = TableHandler.BuildTransactionTable((Table)stepArgument, "Valor");
            }
            else
            {
                throw new NotImplementedException(
                    string.Format("[Error] There isn't a [StepArgumentTransformation] implemented for {0}.",
                        stepArgumentType.Name));
            }

            return TableHandler.TransformTableValues(dataTable, InputStringTransform);
        }

    }
}
