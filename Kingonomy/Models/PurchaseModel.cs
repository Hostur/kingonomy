﻿using System;

namespace Kingonomy.Models
{
    /// <summary>
    /// Purchase is a backend entity that represents in-game transaction.
    /// It can be defined to give player rewards, let him buy something by in-game resources.
    /// Examples:
    /// First login reward - Could contain reward without a price.
    /// Season pass reward - Could contain reward in exchange by season pass experience (resource) and season pass premium (item).
    /// etc.
    /// </summary>
    [Preserve, Serializable]
    public sealed class PurchaseModel
    {
        [Preserve]
        public PurchaseModel(string purchaseId, ItemModel[]? reward, ItemModel[]? price)
        {
            PurchaseId = purchaseId;
            Reward = reward;
            Price = price;
        }

        [Preserve] public PurchaseModel(){}
        [Preserve] public string? PurchaseId { get; set; }
        [Preserve] public ItemModel[]? Reward { get; set; }
        [Preserve] public ItemModel[]? Price { get; set; }
    }
}
