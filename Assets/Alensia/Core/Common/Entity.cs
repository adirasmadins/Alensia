﻿using Alensia.Core.I18n;
using UnityEngine;

namespace Alensia.Core.Common
{
    public class Entity : ManagedMonoBehavior, IEntity
    {
        public string Name => _name;

        public TranslatableText DisplayName => _displayName;

        [SerializeField] private string _name;

        [SerializeField] private TranslatableText _displayName;
    }
}