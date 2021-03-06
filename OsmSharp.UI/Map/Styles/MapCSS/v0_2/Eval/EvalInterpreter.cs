﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Collections.Tags;

namespace OsmSharp.UI.Map.Styles.MapCSS.v0_2.Eval
{
    /// <summary>
    /// An interpreter for MapCSS eval functions based on the definition given in:
    /// http://wiki.openstreetmap.org/wiki/MapCSS/0.2/eval
    /// </summary>
    public class EvalInterpreter
    {
        /// <summary>
        /// Creates a new eval interpreter.
        /// </summary>
        private EvalInterpreter()
        {

        }

        /// <summary>
        /// Holds the one and only instance.
        /// </summary>
        private static EvalInterpreter _instance;

        /// <summary>
        /// Returns the one and only instance.
        /// </summary>
        public static EvalInterpreter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EvalInterpreter();
                }
                return _instance;
            }
        }

        private const string TAG_TOKEN = "tag";

        private const string PROP_TOKEN = "prop";

        private const string COND_TOKEN = "cond";

        private const string ANY_TOKEN = "any";

        private const string MAX_TOKEN = "max";

        private const string MIN_TOKEN = "min";

        /// <summary>
        /// Interpreters the given eval function.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="evalFunction"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public TValue Interpret<TValue>(string evalFunction, TagsCollection tags)
        {
            if (!string.IsNullOrWhiteSpace(evalFunction)) { throw new ArgumentOutOfRangeException("evalFunction cannot be null"); }

            // trim eval function.
            evalFunction = evalFunction.Trim();

            throw new NotImplementedException();
            //return (TValue)this.Interpreter(evalFunction, tags);
        }

        /// <summary>
        /// Interpreters the given expression.
        /// </summary>
        /// <param name="function"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        private string Interpreter(string expression, TagsCollection tags)
        {
            // test for one of the tokens.
            if (expression.StartsWith(TAG_TOKEN))
            { // get the tag value.
                string[] args = this.ParseFunction(expression);
                if (args.Length != 1) { throw new EvalInterpreterException("Invalid argument count: {0}", expression); }
                string tagValue;
                if (tags.TryGetValue(args[0], out tagValue))
                { // the tag value.
                    tagValue = null;
                }
                return tagValue;
            }
            else if (expression.StartsWith(PROP_TOKEN))
            { // not supported for now: mapcss interpretation code needs to change first to keep a list
                // of current properties.
                return null; // return 'none'.
            }
            else if (expression.StartsWith(COND_TOKEN))
            { // evaluate expression and decide on the next expression.
                string[] args = this.ParseFunction(expression);
                if (args.Length != 3) { throw new EvalInterpreterException("Invalid argument count: {0}", expression); }
                if (this.ParseBool(this.Interpreter(args[0], tags)))
                { // evaluate and return the true-part.
                    return this.Interpreter(args[1], tags);
                }
                else
                { // evaluate and return the false-part.
                    return this.Interpreter(args[2], tags);
                }
            }
            else if (expression.StartsWith(ANY_TOKEN))
            { // return the first expression that does not evaluate to 'none'.
                string[] args = this.ParseFunction(expression);
                if (args.Length == 0) { throw new EvalInterpreterException("Invalid argument count: {0}", expression); }
                for (int idx = 0; idx < args.Length; idx++)
                { // evaluate each expression in the correct order.
                    string result = this.Interpreter(args[idx], tags);
                    if (!this.IsNone(result))
                    { // expression was found not returning 'none'.
                        return result;
                    }
                }
                return null;
            }
            else if (expression.StartsWith(MAX_TOKEN))
            { // returns the maximum value of all arguments.

            }
            else if (expression.StartsWith(MIN_TOKEN))
            { // returns the minimum value of all arguments.

            }
            throw new EvalInterpreterException("Failed to evaluate expression: {0}", expression);
        }

        /// <summary>
        /// Evaluate the given operation.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private bool EvaluateOperation(string left, string right, string operation)
        {
            switch (operation)
            {
                case "==": // assume number.
                    return this.EvaluateNumber(left) == this.EvaluateNumber(right);
                case "<":
                    return this.EvaluateNumber(left) < this.EvaluateNumber(right);
                case "<=":
                    return this.EvaluateNumber(left) <= this.EvaluateNumber(right);
                case ">":
                    return this.EvaluateNumber(left) > this.EvaluateNumber(right);
                case ">=":
                    return this.EvaluateNumber(left) >= this.EvaluateNumber(right);
                case "!=": // assume number.
                    return this.EvaluateNumber(left) == this.EvaluateNumber(right);
                case "eq": // assume number.
                    return left == right;
                case "nq": // assume number.
                    return left != right;
                default:
                    throw new EvalInterpreterException("{0} operation not supported!",
                        operation);
            }
        }

        /// <summary>
        /// Evaluates a number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private float EvaluateNumber(string number)
        {
            float result;
            if (!float.TryParse(number, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// Returns true if the given string represents none.
        /// </summary>
        /// <param name="noneString"></param>
        /// <returns></returns>
        private bool IsNone(string noneString)
        {
            return noneString == null || noneString == string.Empty;
        }

        /// <summary>
        /// Returns the interpreted value of the boolstring according to the MapCSS eval boolean datatype rules.
        /// </summary>
        /// <param name="boolString"></param>
        /// <returns></returns>
        private bool ParseBool(string boolString)
        {
            return !(boolString == null || boolString == "false" || boolString == string.Empty || boolString == "no");
        }

        /// <summary>
        /// Parses a given function.
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        private string[] ParseFunction(string function)
        {
            throw new NotImplementedException();
        }
    }
}
