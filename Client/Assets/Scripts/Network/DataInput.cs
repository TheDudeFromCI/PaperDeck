using System.Net.Sockets;

using Be.IO;

namespace PaperDeck.Network
{
    public class DataInput : BeBinaryReader
    {
        public DataInput(NetworkStream stream) : base(stream) {}

        public override string ReadString()
        {
            var count = ReadInt32();

            byte[] bytes = ReadBytes(count * 2);
            char[] chars = new char[count];

            for (int i = 0; i < count; i++)
                chars[i] = (char) ((bytes[i * 2 + 0] << 8) | bytes[i * 2 + 1]);

            return new string(chars);
        }
    }

    public class DataOutput : BeBinaryWriter
    {
        public DataOutput(NetworkStream stream) : base(stream) {}

        public override void Write(string s)
        {
            Write(s.Length);

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                Write((byte) ((c >> 8) & 0xFF));
                Write((byte) (c & 0xFF));
            }
        }
    }
}
