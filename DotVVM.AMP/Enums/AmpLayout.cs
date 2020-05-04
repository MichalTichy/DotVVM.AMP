using System;

namespace DotVVM.AMP.Enums
{
    public enum AmpLayout
    {
        [LayoutRequirements(false, false)]
        container,
        [LayoutRequirements(false, false)]
        noDisplay,
        [LayoutRequirements(true,true)]
        fixedLayout,
        [LayoutRequirements(true, true)]
        responsive,
        [LayoutRequirements(false, true)]
        fixedHeight,
        [LayoutRequirements(false, false)]
        fill,
        [LayoutRequirements(false, false)]
        flexItem,
        [LayoutRequirements(true, true)]
        intrinsic,
    }

    [AttributeUsage(AttributeTargets.All)]
    sealed class LayoutRequirementsAttribute : Attribute
    {
        public bool RequiresWidth { get; }
        public bool RequiresHeight { get; }
        public LayoutRequirementsAttribute(bool requiresWidth, bool requiresHeight)
        {
            RequiresWidth = requiresWidth;
            RequiresHeight = requiresHeight;
        }
    }
}