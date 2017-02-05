using Svelto.ECS.Example.Components.HUD;

namespace Svelto.ECS.Example.Nodes.HUD
{
    public class HUDNode: NodeWithID
    {
        public IAnimatorHUDComponent    HUDAnimator;
        public IDamageHUDComponent      damageImageComponent;
        public IHealthSliderComponent   healthSliderComponent;
        public IBulletsSliderComponent  bulletsSliderComponent;
        public IScoreComponent          scoreComponent;
        public IWaveMessageComponent    waveMessageComponent;
        public IWaveComponent           waveComponent;
        public IZombearsComponent       zombearsComponent;
    }
}
