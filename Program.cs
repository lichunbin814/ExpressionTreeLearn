using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public static class Arg
    {
        public static bool AnyBool { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //無參數（Void方法）
            var metho1Exp = GetMethodExpression<SampleClass>(sample => sample.Method1());
            //方法名稱：Method1
            GetMethodName(metho1Exp);
            //使用的參數：[]
            GetParameterTypes(metho1Exp);

            //有參數（Void方法）
            var metho1WithParmExp = GetMethodExpression<SampleClass>(sample => sample.Method1(Arg.AnyBool));
            //方法名稱：Method1
            GetMethodName(metho1WithParmExp);
            //使用的參數：[boolean]
            GetParameterTypes(metho1WithParmExp);

            //無參數（回傳字串）
            var getResultExp = GetMethodExpression<SampleClass>(sample => sample.GetResult());
            //方法名稱：GetResult
            GetMethodName(getResultExp);
            //使用的參數：[]
            GetParameterTypes(getResultExp);

            //有參數（回傳字串）
            var getResultWithParmExp = GetMethodExpression<SampleClass>(sample => sample.GetResult(Arg.AnyBool));
            //方法名稱：GetResult
            GetMethodName(getResultWithParmExp);
            //使用的參數：[boolean]
            GetParameterTypes(getResultWithParmExp);
        }

        /// <summary>
        /// 用於Void方法
        /// </summary>
        static MethodCallExpression GetMethodExpression<TClass>(Expression<Action<TClass>> expression)
            where TClass : class
        {
            var methodExp = (MethodCallExpression)expression.Body;
            return methodExp;
        }

        /// <summary>
        /// 用於有結果的方法
        /// </summary>
        static MethodCallExpression GetMethodExpression<TClass>(Expression<Action<TClass , object>> expression)
            where TClass : class
        {
            var methodExp = (MethodCallExpression)expression.Body;
            return methodExp;
        }

        /// <summary>
        /// 取得方法的名稱
        /// </summary>
        static string GetMethodName(MethodCallExpression methodExp)
        {
            return methodExp.Method.Name;
        }

        /// <summary>
        /// 取得方法的參數類型
        /// </summary>
        static Type[] GetParameterTypes(MethodCallExpression methodExp)
        {
            //取得參數
            var methodParameters = methodExp.Method.GetParameters();
            //轉為Type
            Type[] actionParmTypes = methodParameters.Select(parm => parm.ParameterType).ToArray();
            //回傳
            return actionParmTypes;
        }
    }

    public class SampleClass
    {
        public void Method1()
        {
            //....
        }

        public void Method1(bool isBool)
        {
            //...
        }

        public string GetResult()
        {
            //...
            return "Result";
        }

        public string GetResult(bool isBool)
        {
            //....
            return "Result";
        }
    }
}
