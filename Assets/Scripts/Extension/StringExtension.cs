using System.Text;

namespace Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// show as hh : mm : ss
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string SecondToTimeString_HMS(double seconds)
        {
            StringBuilder time = new StringBuilder();

            int hour = (int)(seconds / 3600);
            int min = (int)(seconds / 60) - (hour * 60);
            int sec = (int)(seconds % 60);

            time.Append(hour < 10 ? "0" + hour : hour);
            time.Append(' ');
            time.Append(':');
            time.Append(' ');
            time.Append(min < 10 ? "0" + min : min);
            time.Append(' ');
            time.Append(':');
            time.Append(' ');
            time.Append(sec < 10 ? "0" + sec : sec);
            return time.ToString();
        }

        /// <summary>
        /// show as mm : ss
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string SecondToTimeString_MS(double seconds)
        {
            StringBuilder time = new StringBuilder();
            int min = (int)(seconds / 60);
            int sec = (int)(seconds % 60);

            time.Append(min < 10 ? "0" + min : min);
            time.Append(' ');
            time.Append(':');
            time.Append(' ');
            time.Append(sec < 10 ? "0" + sec : sec);
            return time.ToString();
        }
    }
}