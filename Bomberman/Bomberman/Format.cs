using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    public class Format
    {
        /// <summary>
        /// HH:mm 形式の文字列に変換して返します
        /// </summary>
        public string ChangeFormatToMMSS(Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("mm:ss");
        }

        /// <summary>
        /// HH:mm 形式の文字列に変換して返します
        /// </summary>
        public string ChangeFormatToMMSS(string seconds_str)
        {
            int seconds = int.Parse(seconds_str);
            int s = seconds % 60;
            int m = (seconds - s) / 60;

            string s_str;
            string m_str;
            if (s < 10) s_str = "0" + s.ToString();
            else s_str = s.ToString();
            if (m < 10) m_str = "0" + m.ToString();
            else m_str = m.ToString();
            return m_str + ":" + s_str;
           
        }
    }
}
