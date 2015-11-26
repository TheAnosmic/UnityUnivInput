using System;

namespace UnityEngine.UI.Univ
{
    public class UnivTextPart
    {
        public string Text
        {
            get { return reversedCache ?? _mText; }
            private set { _mText = value; }
        }

        public bool isRTL { get; private set; }

        private string _mText;
        private string reversedCache;

        public UnivTextPart(string text, bool isRtl = false, bool isGroupRtl = false)
        {
            Text = text;
            isRTL = isRtl;
            if (isRtl)
            {
                var charArray = Text.ToCharArray();
                Array.Reverse(charArray);
                reversedCache = new string(charArray);
                if (!isGroupRtl)
                {
                    while (reversedCache.StartsWith(" "))
                    {
                        reversedCache = reversedCache.Remove(0, 1) + ' ';
                    }
                }
            }
        }
    }

}