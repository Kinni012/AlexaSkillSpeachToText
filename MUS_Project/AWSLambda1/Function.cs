using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

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

    private string CreateFileIntent(IntentRequest intentRequest, ILambdaLogger logger)
    {
      string result = "Created File: ";
      if (!(intentRequest.Intent.Slots.TryGetValue("name", out var fileName))) return result;
      if (string.IsNullOrEmpty(fileName.Value)) return result;
      return result + $" {fileName.Value}";
    }
    private string ConsoleWriteLineIntent(IntentRequest intentRequest, ILambdaLogger logger)
    {
      string result = "Write Line:";
      if (!(intentRequest.Intent.Slots.TryGetValue("text", out var text))) return result;
      if (string.IsNullOrEmpty(text.Value)) return result;
      return result + $" {text.Value}";
    }

    private string GoToLineIntent(IntentRequest intentRequest, ILambdaLogger logger)
    {
      string result = "Go to line:";
      if (!(intentRequest.Intent.Slots.TryGetValue("lineNumber", out var lineNumber))) return result;
      if (string.IsNullOrEmpty(lineNumber.Value)) return result;
      return result + $" {lineNumber.Value}";
    }

    private string GoToColumnIntent(IntentRequest intentRequest, ILambdaLogger logger)
    {
      string result = "Go to column:";
      if (!(intentRequest.Intent.Slots.TryGetValue("columnNumber", out var columnNumber))) return result;
      if (string.IsNullOrEmpty(columnNumber.Value)) return result;
      return result + $" {columnNumber.Value}";
    }

    private string ForIntent(IntentRequest intentRequest, ILambdaLogger logger)
    {
      string result = "For Created, Parameters: ";

      string textFor = "";
      textFor += "for(";
      intentRequest.Intent.Slots.TryGetValue("lowerBount", out var lowerBound);
      intentRequest.Intent.Slots.TryGetValue("upperBound", out var upperBound);
      intentRequest.Intent.Slots.TryGetValue("stepWidth", out var stepWidth);
      intentRequest.Intent.Slots.TryGetValue("variable", out var var);

      string curVarName = "";

      if (!string.IsNullOrEmpty(var.Value))
      {
        textFor += $"int {var.Value} = ";
        curVarName = $"{var.Value}";
        result += "variable: " + curVarName;
      }
      else
      {
        textFor += "int i = ";
        curVarName = "i";
      }

      if (!string.IsNullOrEmpty(lowerBound.Value))
      {
        textFor += $"{lowerBound.Value};";
        result += " lower bound: " + lowerBound.Value;
      }
      else
        textFor += "0;";

      if (!string.IsNullOrEmpty(upperBound.Value))
      {
        textFor += $"{curVarName} < {upperBound.Value};";
        result += " upper bound: " + upperBound.Value;
      }
      else
        textFor += "0;";

      if (!string.IsNullOrEmpty(stepWidth.Value))
      {
        textFor += $"{curVarName} = {curVarName} + {stepWidth.Value};";
        result += " step width: " + stepWidth.Value;
      }
      else
        textFor += "{curVarName}++";

      textFor += @"){" + System.Environment.NewLine + System.Environment.NewLine + "}";


      return result;
    }




    private SkillResponse HandleIntent(IntentRequest intentRequest, ILambdaLogger logger)
    {
      logger.LogLine($"IntentRequest {intentRequest.Intent.Name} made");

      string s = intentRequest.Intent.Name;
      string responseSpeech = "";
      switch (s)
      {
        case "CreateFileIntent":
          responseSpeech += CreateFileIntent(intentRequest, logger);
          break;

        case "ConsoleWriteLineIntent":
          responseSpeech += ConsoleWriteLineIntent(intentRequest, logger);
          break;

        case "GoToLineIntent":
          responseSpeech += GoToLineIntent(intentRequest, logger);
          break;

        case "GoToColumnIntent":
          responseSpeech += GoToColumnIntent(intentRequest, logger);
          break;
          
        case "ForIntent":
          responseSpeech += ForIntent(intentRequest, logger);
          break;

      }


      var response = ResponseBuilder.Tell(new PlainTextOutputSpeech()
      {
        Text = responseSpeech
      });

      return response;
    }
  }
}
