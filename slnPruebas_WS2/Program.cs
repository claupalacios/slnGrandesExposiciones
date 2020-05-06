using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;


namespace slnPruebas_WS2 {
    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            
            List<List<MemBlock>> memA = new List<List<MemBlock>>();
            List<List<MemBlock>> memB = new List<List<MemBlock>>();
            List<List<MemBlock>> memC = new List<List<MemBlock>>();

            while (true) {
                try {
                    for (int i = 0; i < int.MaxValue; i++) {
                        memA.Add(new List<MemBlock>());
                        memB.Add(new List<MemBlock>());

                        for (int j = 0; j < int.MaxValue; j++) {
                            memA[i].Add(new MemBlock());
                            memB[i].Add(new MemBlock());

                            if ((j % 1000) == 0) {
                                Thread.Sleep(100);
                            }
                        }
                    }
                    if (memB.Count >= int.MaxValue) {
                        break;
                    }
                }catch {
                    break;
                }
            }

            while (true) {
                for (int i = 0; i < int.MaxValue; i++) {
                    memC.Add(new List<MemBlock>());

                    for (int j = 0; j < int.MaxValue; j++) {
                        memC[i].Add(new MemBlock());

                        if ((j % 1000) == 0) {
                            Thread.Sleep(100);
                        }
                    }

                }
                if (memC.Count >= int.MaxValue) {
                    memC.Clear();
                    Thread.Sleep(1000);
                }
            }
        }
    }

    class MemBlock {
        private int min;
        private int max;
        private object obj;

        public MemBlock() {
            this.min = int.MinValue;
            this.max = int.MaxValue;
            this.obj = this.GetType();
        }

        public int GetMin() { return this.min; }
        public int GetMax() { return this.max; }
        public object GetObj() { return this.obj; }
    }
}
