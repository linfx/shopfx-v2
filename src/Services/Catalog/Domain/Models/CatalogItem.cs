﻿using Catalog.Domain.Exceptions;
using LinFx.Domain.Models;
using System;

namespace Catalog.Domain.Models
{
    public class CatalogItem : AggregateRoot<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int CatalogTypeId { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public int CatalogBrandId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public CatalogType CatalogType { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public CatalogBrand CatalogBrand { get; set; }

        /// <summary>
        /// 库存数量 Quantity in stock
        /// </summary>
        public int AvailableStock { get; set; }

        /// <summary>
        /// Available stock at which we should reorder
        /// </summary>
        public int RestockThreshold { get; set; }

        /// <summary>
        /// 最大库存数 Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
        /// </summary>
        public int MaxStockThreshold { get; set; }

        /// <summary>
        /// True if item is on reorder
        /// </summary>
        public bool OnReorder { get; set; }

        /// <summary>
        /// Decrements the quantity of a particular item in inventory and ensures the restockThreshold hasn't
        /// been breached. If so, a RestockRequest is generated in CheckThreshold. 
        /// 
        /// If there is sufficient stock of an item, then the integer returned at the end of this call should be the same as quantityDesired. 
        /// In the event that there is not sufficient stock available, the method will remove whatever stock is available and return that quantity to the client.
        /// In this case, it is the responsibility of the client to determine if the amount that is returned is the same as quantityDesired.
        /// It is invalid to pass in a negative number. 
        /// </summary>
        /// <param name="quantityDesired"></param>
        /// <returns>int: Returns the number actually removed from stock. </returns>
        /// 
        public int RemoveStock(int quantityDesired)
        {
            if (AvailableStock == 0)
            {
                throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
            }

            if (quantityDesired <= 0)
            {
                throw new CatalogDomainException($"Item units desired should be greater than cero");
            }

            int removed = Math.Min(quantityDesired, AvailableStock);

            AvailableStock -= removed;

            return removed;
        }

        /// <summary>
        /// Increments the quantity of a particular item in inventory.
        /// <param name="quantity"></param>
        /// <returns>int: Returns the quantity that has been added to stock</returns>
        /// </summary>
        public int AddStock(int quantity)
        {
            int original = AvailableStock;

            // The quantity that the client is trying to add to stock is greater than what can be physically accommodated in the Warehouse
            if ((AvailableStock + quantity) > MaxStockThreshold)
            {
                // For now, this method only adds new units up maximum stock threshold. In an expanded version of this application, we
                //could include tracking for the remaining units and store information about overstock elsewhere. 
                AvailableStock += MaxStockThreshold - AvailableStock;
            }
            else
            {
                AvailableStock += quantity;
            }

            OnReorder = false;

            return AvailableStock - original;
        }
    }
}
