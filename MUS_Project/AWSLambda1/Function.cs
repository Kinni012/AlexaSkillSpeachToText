using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System.Net.Http;
using System.Text;
using System.IO;
using AWSLambda1.HttpHandler;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var logger = context.Logger;

            switch (input.Request)
            {
                case LaunchRequest launchRequest: return HandleLaunch(launchRequest, logger);
                case IntentRequest intentRequest: return HandleIntent(intentRequest, logger);
            }

            throw new NotImplementedException("Unknown request type.");
        }

        private SkillResponse HandleLaunch(LaunchRequest launchRequest, ILambdaLogger logger)
        {
            logger.LogLine($"LaunchRequest made");
            var response = ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = $"Welcome! This is a perfect working speach to program program."
            });

            return response;
        }

        private string ConsoleWriteLineIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Write Line:";
            if (!(intentRequest.Intent.Slots.TryGetValue("text", out var text))) return result;
            if (string.IsNullOrEmpty(text?.Value)) return result;
            var x = new { text = text.Value };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/ConsoleWriteLine", data);
            return result + $" {text.Value}";
        }

        private string ConsoleWriteLineVariableIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Write line variable:";

            if (!(intentRequest.Intent.Slots.TryGetValue("varName", out var varName))) return result;
            if (string.IsNullOrEmpty(varName?.Value)) return result;


            var x = new { varName = varName.Value };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/ConsoleWriteLineVariable", data);

            return result + $" {varName.Value}";
        }

        private string GoToLineIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Go to line:";
            if (!(intentRequest.Intent.Slots.TryGetValue("lineNumber", out var lineNumber))) return result;
            if (string.IsNullOrEmpty(lineNumber?.Value)) return result;

            var x = new { lineNumber = lineNumber.Value };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/SetRow", data);

            return result + $" {lineNumber.Value}";
        }

        private string DeleteLineIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Delete Line:";
            if (!(intentRequest.Intent.Slots.TryGetValue("lineNr", out var lineNr))) return result;
            if (string.IsNullOrEmpty(lineNr?.Value)) return result;

            var x = new { lineNr = lineNr.Value };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/DeleteLine", data);
            return result + $" {lineNr.Value}";
        }
        private string IncreaseVarIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Increase Variable:";
            if (!(intentRequest.Intent.Slots.TryGetValue("variableName", out var variableName))) return result;
            if (string.IsNullOrEmpty(variableName?.Value)) return result;

            var x = new { variableName = variableName.Value };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/IncreaseVar", data);

            return result + $" {variableName.Value}";
        }


        private string GernericInputIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Generic Input:";
            if (!(intentRequest.Intent.Slots.TryGetValue("anyString", out var anyString))) return result;
            if (string.IsNullOrEmpty(anyString?.Value)) return result;
            return result + $" {anyString.Value}";
        }


        private string ReadFileIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            return HttpHelper.PerformPost(Settings.PostUrl + "/ReadFile", "\"Test.txt\"");
        }

        private string ResetFileIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            return "Reset file";
        }



        private string GoToColumnIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Go to column:";
            if (!(intentRequest.Intent.Slots.TryGetValue("columnNumber", out var columnNumber))) return result;
            if (string.IsNullOrEmpty(columnNumber?.Value)) return result;

            var x = new { columnNumber = columnNumber.Value };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/SetColumn", data);

            return result + $" {columnNumber.Value}";
        }
        private string CreateBoolVariableIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "CreatedBool:";
            intentRequest.Intent.Slots.TryGetValue("name", out var name);
            intentRequest.Intent.Slots.TryGetValue("value", out var value);

            if (!(string.IsNullOrEmpty(name?.Value)))
                result += $" {name.Value}";

            if (!(string.IsNullOrEmpty(value?.Value)))
                result += $" = {value.Value}";


            return result;
        }

        private string CreateIntVariableIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "Created int:";
            intentRequest.Intent.Slots.TryGetValue("name", out var name);
            intentRequest.Intent.Slots.TryGetValue("value", out var value);

            if (!(string.IsNullOrEmpty(name?.Value)))
                result += $" {name.Value}";

            if (!(string.IsNullOrEmpty(value?.Value)))
                result += $" = {value.Value}";


            return result;
        }


        private string ForIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "For Created, Parameters: ";
            string lBound;
            string uBound;
            string sWidth;
            string var;


            intentRequest.Intent.Slots.TryGetValue("lowerBount", out var lowerBound);
            intentRequest.Intent.Slots.TryGetValue("upperBound", out var upperBound);
            intentRequest.Intent.Slots.TryGetValue("stepWidth", out var stepWidth);
            intentRequest.Intent.Slots.TryGetValue("variable", out var variable);

            if (!string.IsNullOrEmpty(variable?.Value))
            {
                result += "variable: " + variable.Value;
                var = variable.Value;
            }
            else
            {
                result += "variable: i";
                var = "i";
            }

            if (!string.IsNullOrEmpty(lowerBound?.Value))
            {
                result += " lower bound: " + lowerBound.Value;
                lBound = lowerBound.Value;
            }
            else
            {
                result += " lower bound: 0";
                lBound = "0";
            }

            if (!string.IsNullOrEmpty(upperBound?.Value))
            {
                result += " upper bound: " + upperBound.Value;
                uBound = upperBound.Value;
            }
            else
            {
                result += " upper bound: 10";
                uBound = "10";
            }

            if (!string.IsNullOrEmpty(stepWidth?.Value))
            {
                result += " step width: " + stepWidth.Value;
                sWidth = stepWidth.Value;
            }
            else
            {
                result += " step width: 1";
                sWidth = "1";
            }



            var x = new { varName = var, lowerBound = lBound, upperBound = uBound, stepWidth = sWidth };
            var data = JsonConvert.SerializeObject(x);

            HttpHelper.PerformPost(Settings.PostUrl + "/CreateFor", data);

            return result;
        }

        private string WhileIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "While Created, Parameters: ";
            if ((intentRequest.Intent.Slots.TryGetValue("varName", out var varName)) &&
            (intentRequest.Intent.Slots.TryGetValue("compareType", out var compareType)) &&
            (intentRequest.Intent.Slots.TryGetValue("num", out var num)))
            {

                if (!string.IsNullOrEmpty(varName?.Value))
                    result += " var name: " + varName.Value;

                if (!string.IsNullOrEmpty(compareType?.Value))
                    result += " compareType: " + compareType.Value;

                if (!string.IsNullOrEmpty(num?.Value))
                    result += " num: " + num.Value;

                var x = new { varName = varName.Value, compareType = convertCompareTextToSymbol(compareType.Value), number = num.Value };
                var data = JsonConvert.SerializeObject(x);

                HttpHelper.PerformPost(Settings.PostUrl + "/CreateWhile", data);
            }
            return result;
        }

        private string IfIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            string result = "If Created, Parameters: ";
            if ((intentRequest.Intent.Slots.TryGetValue("varName", out var varName)) &&
            (intentRequest.Intent.Slots.TryGetValue("compareType", out var compareType)) &&
            (intentRequest.Intent.Slots.TryGetValue("number", out var number)))
            {

                if (!string.IsNullOrEmpty(varName?.Value))
                    result += " var name: " + varName.Value;

                if (!string.IsNullOrEmpty(compareType?.Value))
                    result += " compareType: " + compareType.Value;

                if (!string.IsNullOrEmpty(number?.Value))
                    result += " number: " + number.Value;


                var x = new { varName = varName.Value, compareType = convertCompareTextToSymbol(compareType.Value), number = number.Value };
                var data = JsonConvert.SerializeObject(x);

                HttpHelper.PerformPost(Settings.PostUrl + "/CreateIf", data);
            }
            return result;
        }


        //Use Url in Settingsfile for each post request
        private SkillResponse HandleIntent(IntentRequest intentRequest, ILambdaLogger logger)
        {
            logger.LogLine($"IntentRequest {intentRequest.Intent.Name} made");

            string s = intentRequest.Intent.Name;
            string responseSpeech = "";
            switch (s)
            {
                case "ConsoleWriteLineIntent":
                    responseSpeech += ConsoleWriteLineIntent(intentRequest, logger);
                    break;

                case "ConsoleWriteLineVariableIntent":
                    responseSpeech += ConsoleWriteLineVariableIntent(intentRequest, logger);
                    break;



                case "GoToLineIntent":
                    responseSpeech += GoToLineIntent(intentRequest, logger);
                    break;

                case "GoToColumnIntent":
                    responseSpeech += GoToColumnIntent(intentRequest, logger);
                    break;

                case "CreateBoolVariableIntent":
                    responseSpeech += CreateBoolVariableIntent(intentRequest, logger);
                    break;

                case "CreateIntVariableIntent":
                    responseSpeech += CreateIntVariableIntent(intentRequest, logger);
                    break;


                case "ForIntent":
                    responseSpeech += ForIntent(intentRequest, logger);
                    break;

                case "WhileIntent":
                    responseSpeech += WhileIntent(intentRequest, logger);
                    break;

                case "IfIntent":
                    responseSpeech += IfIntent(intentRequest, logger);
                    break;

                case "ReadFileIntent":
                    responseSpeech += ReadFileIntent(intentRequest, logger);
                    break;

                case "DeleteRowIntent":
                    responseSpeech += DeleteLineIntent(intentRequest, logger);
                    break;

                case "IncreaseVarIntent":
                    responseSpeech += IncreaseVarIntent(intentRequest, logger);
                    break;

                case "ResetFileIntent":
                    responseSpeech += ResetFileIntent(intentRequest, logger);
                    break;

                case "GernericInputIntent":
                    responseSpeech += GernericInputIntent(intentRequest, logger);
                    break;


            }


            var response = ResponseBuilder.Tell(new PlainTextOutputSpeech()
            {
                Text = responseSpeech
            });


            return response;
        }

        string convertCompareTextToSymbol(string s)
        {
            switch (s.ToLower())
            {
                case "lesser":
                    return "<";
                case "higher":
                    return ">";
                case "equals":
                    return "==";
                case "higher or equals":
                    return ">=";
                case "lesser or equals":
                    return "<=";
            }
            return "==";
        }
    }
}
