using System.Linq;

namespace Discord.Addons.Linking
{
    internal class MessageContent
    {
        public MessageContent() { }

        public MessageContent(IMessage message)
        {
            Content = message.Content;
            IsTTS = message.IsTTS;
            Embed = GetRichEmbed(message);
        }

        public string Content { get; set; }
        
        public bool IsTTS { get; set; }

        public EmbedBuilder Embed { get; set; }

        public static EmbedBuilder GetRichEmbed(IMessage message)
        {
            return message?.Embeds.FirstOrDefault(x => x.Type == EmbedType.Rich)?.ToEmbedBuilder();
        }
    }
}
