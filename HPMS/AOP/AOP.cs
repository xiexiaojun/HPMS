using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Threading;

namespace HPMS.AOP
{
    [Flags]
    public enum JoinType
    {
        Permission=1,
        Log=2
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PermissonAttribute : Attribute
    {
        public  string Role { get; set; }

        public PermissonAttribute(string role)
        {
            this.Role = role;
        }

     
    }

    //贴上类的标签
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    //当对一个类应用 sealed 修饰符时，此修饰符会阻止其他类从该类继承
    //ContextAttribute:构造器带有一个参数，用来设置ContextAttribute的名称。
    public sealed class PermissonCheckAttribute : ContextAttribute,
        IContributeObjectSink
    {
        public JoinType JoinType { get; set; }
        public PermissonCheckAttribute(JoinType joinType)
            : base("")
        {
            this.JoinType = joinType;
        }

       


        //实现IContributeObjectSink接口当中的消息接收器接口
        public System.Runtime.Remoting.Messaging.IMessageSink GetObjectSink(MarshalByRefObject obj, System.Runtime.Remoting.Messaging.IMessageSink nextSink)
        {
            return new MyAopHandler(nextSink);
        }
    }
    public sealed class MyAopHandler : IMessageSink
    {

        //下一个接收器
        private IMessageSink nextSink;

        public MyAopHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        public IMessageSink NextSink
        {
            get { return nextSink; }
        }

        //同步处理方法
        public IMessage SyncProcessMessage(IMessage msg)
        {

            IMessage message = null;

            //方法调用接口
            IMethodCallMessage callMessage = msg as IMethodCallMessage;

            //如果被调用的方法没打MyCalculatorMethodAttribute标签
            if (callMessage == null || (Attribute.GetCustomAttribute(callMessage.MethodBase, typeof(PermissonAttribute))) == null)
            {
                message = nextSink.SyncProcessMessage(msg);
            }
            else
            {
               
                var attribute = (PermissonAttribute)callMessage.MethodBase.GetCustomAttributes(typeof(PermissonAttribute), false).FirstOrDefault();

                if (attribute == null)
                {
                    return null;
                }

                string aa = (string) attribute.Role;
                IPrincipal threadPrincipal = Thread.CurrentPrincipal;
                bool bbb=threadPrincipal.IsInRole(aa);
                if (bbb)
                {
                    PreProceed(msg);
                    message = nextSink.SyncProcessMessage(msg);
                    PostProceed(message);
                }
                else
                {
                    throw new Exception(string.Format("角色{0}没有访问操作{1}的权限！", threadPrincipal.Identity.AuthenticationType, aa));
                }
               
               
            }

            return message;
        }

        //异步处理方法
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            Console.WriteLine("异步处理方法...");
            return null;
        }

        //方法执行前
        public void PreProceed(IMessage msg)
        {
            IMethodMessage message = (IMethodMessage)msg;
            Console.WriteLine("New Method Start...");
            Console.WriteLine("This Method Is {0}", message.MethodName);
            Console.WriteLine("This Method A Total of {0} Parameters Including:", message.ArgCount);
            for (int i = 0; i < message.ArgCount; i++)
            {
                Console.WriteLine("Parameter{0}：The Args Is {1}.", i + 1, message.Args[i]);
            }
        }

        //方法执行后
        public void PostProceed(IMessage msg)
        {
            IMethodReturnMessage message = (IMethodReturnMessage)msg;

            Console.WriteLine("The Return Value Of This Method Is {0}", message.ReturnValue);
            Console.WriteLine("Method End\n");
        }
    }
}
