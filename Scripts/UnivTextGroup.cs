using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.UI.Univ
{
    public class UnivTextGroup
    {
        private static readonly string RTLChars = "אבגדהוזחטיכךלמםנןסעפףצץקרשת";
        private static readonly string AgnosticChars = " \r\n";

        public IList<UnivTextPart> Parts { get; private set; }

        public bool IsGroupRTL;

        public static bool IsCharRTL(char c)
        {
            return RTLChars.Contains(c);
        }

        public UnivTextGroup(string fullText)
        {
            Parts = new List<UnivTextPart>();

            if (string.IsNullOrEmpty(fullText))
            {
                return;
            }

            IsGroupRTL = IsCharRTL(fullText[0]);

            var builder = new StringBuilder();
            var currentRTL = IsGroupRTL;

            for (var i = 0; i < fullText.Length; i++)
            {
                var currentChar = fullText[i];
                var isAgnosticChar = AgnosticChars.Contains(currentChar);
                if (IsCharRTL(currentChar) != currentRTL && !isAgnosticChar)
                {
                    Parts.Add(new UnivTextPart(builder.ToString(), currentRTL, IsGroupRTL));
                    builder.Length = 0;
                    currentRTL = !currentRTL;
                }
                builder.Append(currentChar);
            }

            if (builder.Length > 0)
            {
                Parts.Add(new UnivTextPart(builder.ToString(), currentRTL, IsGroupRTL));
            }
        }

        public string CorrectedString()
        {
            if (Parts.Count == 0)
            {
                return "";
            }


            StringBuilder sb = new StringBuilder();

            if (IsGroupRTL)
            {
                for (var i = Parts.Count - 1; i >= 0; i--)
                {
                    sb.Append(Parts[i].Text);
                }
            }
            else
            {
                for (var i = 0; i < Parts.Count; i++)
                {
                    sb.Append(Parts[i].Text);
                }
            }

            return sb.ToString();
        }
    }
}