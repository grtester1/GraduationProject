using Xamarin.Forms;

namespace AgentVI.Custom.Effects
{
    public class UnderlineEffect : RoutingEffect
    {
        public const string EffectNamespace = "UnderlineEffect";

        public UnderlineEffect() : base($"{EffectNamespace}.{nameof(UnderlineEffect)}")
        {

        }
    }
}
