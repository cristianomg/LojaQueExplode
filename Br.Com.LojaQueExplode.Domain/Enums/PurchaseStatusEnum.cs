using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Domain.Enums
{
    public enum PurchaseStatusEnum
    {
        Open,
        RequestedProducts,
        PaymentMade,
        PaymentApproved,
        SendedProducts,
        PurchaseFinished
    }
}
