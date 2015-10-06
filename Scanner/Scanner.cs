using System;
using System.Collections.Generic;


namespace Utilities
{
    public class Scanner
    {
        private Queue<String> m_buffer = new Queue<string>();
        private void FlushConsole()
        {
            if(m_buffer.Count == 0)
            {
                string[] str = Console.ReadLine().Split(' ');
                foreach(string s in str)
                {
                    m_buffer.Enqueue(s);
                }

            }
            
        }

        public void SkipNext()
        {
            m_buffer.Dequeue();
        }
        public void Clear()
        {
            m_buffer.Clear();
        }


        #region Next Value Checkers

        private TypeCode cacheType;

        //Is used to temporarily store values parsed by the IsNext() methods as boxing/unboxing is far faster than parsing the string again. 
        private System.Object cache;

        public bool HasNext()
        {
            FlushConsole();
            return m_buffer.Count > 0;
        }
        public bool IsNextInt()
        {
            FlushConsole();
            int value = 0;
            if(int.TryParse(m_buffer.Peek(), out value))
            {
                //We use cache since users will most likely acess this variable next, if true. No need to parse a full string twice. 
                cacheType = TypeCode.Int32;
                cache = value;
                return true;
            }
            return false;
        }
        public bool IsNextString()
        {
            return true;
        }
        public bool IsNextDouble()
        {
            FlushConsole();
            double value = 0;
            if(double.TryParse(m_buffer.Peek(), out value))
            {
                cacheType = TypeCode.Double;
                cache = value;
                return true;
            }
            return false;
        }
        public bool IsNextLong()
        {
            FlushConsole();
            long value = 0;
            if(long.TryParse(m_buffer.Peek(), out value))
            {
                cacheType = TypeCode.Int64;
                cache = value;
                return true;
            }
            return false;
        }
        #endregion
        #region Getters
        public string GetNext()
        {
            FlushConsole();
            return m_buffer.Dequeue();
        }
        public int GetNextInt()
        {
            FlushConsole();
            if (cache != null)
            {
                //Checks if the cache contains this type
                if(cacheType == TypeCode.Int32)
                {
                    m_buffer.Dequeue();
                    return (int)cache;
                }


                //If it wasn't this cache type, then the cache becomes invalid because of the next convert
                cache = null;
            }
            return Convert.ToInt32(m_buffer.Dequeue());
          
        }
        public double GetNextDouble()
        {
            FlushConsole();
            if (cache != null)
            {
                //Checks if the cache contains this type
                if (cacheType == TypeCode.Double)
                {
                    m_buffer.Dequeue();
                    return (double)cache;
                }


                //If it wasn't this cache type, then the cache becomes invalid because of the next convert
                cache = null;
            }
            return Convert.ToDouble(m_buffer.Dequeue());
        }
        public long GetNextLong()
        {
            FlushConsole();
            if (cache != null)
            {
                //Checks if the cache contains this type
                if (cacheType == TypeCode.Int64)
                {
                    m_buffer.Dequeue();
                    return (long)cache;
                }


                //If it wasn't this cache type, then the cache becomes invalid because of the next convert
                cache = null;
            }
            return Convert.ToInt64(m_buffer.Dequeue());
        }
        #endregion

    }
}
