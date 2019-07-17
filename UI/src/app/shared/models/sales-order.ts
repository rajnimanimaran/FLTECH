export class SalesOrderMaster {
    OrderID: number;
    ID: number;
    OrderNumber: number;
    CODE: string;
    prdID:number;
    // SID: number;
    CustId: number;
    OrderTakenBy:string;
    OrderStatus:number;
    // SName:string;
    OrderDate: Date;
    ACKDate: Date;
    AssignedTo: string;
    OrderDetails: string;
    materialType: number;
    AssignedQty:number;
    AlreadyAssignedQty:number;
    BalanceQty:number;
    WorkStatus:number;
}



// export class SALESFORM {
//     OrderNumber: number;
//     CODE: string;
//     CustomerID: number;
//     OrderDate: number;
//     ACKDate: string;
//     AssignedTo: string;
//     OrderDetails : string;
//     OrderTakenBy : string;
//     OrderFormDetail: SALESFORMDETAILS[];
// }

// export class SALESFORMDETAILS {
//     prdID: number;
//     Quantity: string;
//     UOMID: number;
//     GSTNO: number;
//     Rate: number;
//     Tax:number;
//     Amount: number;
//     T_Amount: number
// }