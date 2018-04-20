using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class MyEventArgs : EventArgs
    {
        public int p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public MyEventArgs(int p1, string p2, string p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

    }
    public class Publisher
    {
        public delegate void TempHandler(object o, MyEventArgs e);
        public event TempHandler TempEvent;

        /// <summary>
        /// 获得订阅方法列表
        /// </summary>
        public IList<MethodInfo> GetSubList()
        {
             IList< MethodInfo > mList = new List<MethodInfo>();
            // return
            var list = TempEvent.GetInvocationList();
            foreach (var i in list)
            {
                mList.Add(i.Method);
            }
            return mList;
        }

        /// <summary>
        /// 运行方法
        /// </summary>
        public void Tempchange()
        {
            int i = 1000;
            while (i > 0)
            {
                Thread.Sleep(1);
                i--;
                TempEvent(this, new MyEventArgs(i, "", ""));
                if (i == 0)
                {
                    //FinishedEvent();
                }
            }

        }
    }
}
