﻿using Exiled.API.Enums;

namespace UncomplicatedCustomRoles.Structures
{
    public interface IUCREffect
    {
        public abstract EffectType EffectType { get; set; }
        public abstract float Duration { get; set; }
        public abstract byte Intensity { get; set; }
    }
}