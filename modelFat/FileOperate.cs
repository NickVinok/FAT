using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace modelFat
{
    class FileOperate<T>
    {
        //<summary>
        //Размер 1 кластера в битах
        //</summary>
        public int sizeBlock = 1024;
        FileList<T> LF;
        public FileOperate(FileList<T> LF)
        {
            this.LF = LF;
        }

        public BitArray[] Separate(byte[] f)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            BitArray bitFile = new BitArray(f);
            
            var chank = Convert.ToInt32(Math.Round(bitFile.Length / (double)sizeBlock));
            BitArray[] bitBlocks = new BitArray[chank];
            
            for (int i = 0; i < chank; i++)
            {

                BitArray tmp = new BitArray(length: sizeBlock);
                int startJ = i == 0 ? 0 : i * sizeBlock;
                for (int j = 0; j < sizeBlock; j++) {
                    try
                    {
                        tmp[j] = bitFile[j + startJ];
                    }
                    catch (Exception)
                    {                        
                        continue;
                    }
                }
                bitBlocks.SetValue(tmp, i);                
            }

                return bitBlocks;
        }

        private List<T> File = new List<T>() { };

        public List<T> Combine(int inx)
        {
            GetData(inx);
            return File;
        }
        // тут я поехал хз что наделал шел который час разработки 
        private T GetData(int inx)
        {
            Dictionary<int, T> f = LF.GetPartFileByInx(inx);
            var fk = f.FirstOrDefault().Key;
            if (fk != -3)
            {
                File.Add(f.FirstOrDefault().Value);
                GetData(fk);
            }
            else
            {
                File.Add(f.FirstOrDefault().Value);
            }
            
            return f.FirstOrDefault().Value;
        }

    }
}
