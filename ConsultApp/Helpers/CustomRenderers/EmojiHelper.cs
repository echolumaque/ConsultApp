using System.Text;

namespace ConsultApp.Helpers.CustomRenderers
{
    public class EmojiHelper
    {
        private readonly int[] codes;

        public EmojiHelper(int[] codes)
        {
            this.codes = codes;
        }

        public EmojiHelper(int code)
        {
            codes = new int[] { code };
        }

        public override string ToString()
        {
            if (codes == null)
                return string.Empty;

            var sb = new StringBuilder(codes.Length);

            foreach (var code in codes)
                sb.Append(char.ConvertFromUtf32(code));

            return sb.ToString();
        }
    }
}
