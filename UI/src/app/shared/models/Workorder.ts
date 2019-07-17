export class WorkOrder{
    WorkOrderID?: number;
    OrderID: string;
    ContID: number;
    AssignedDate: Date;
    ExpectedDeliveryDate: Date;
    isActive:number=1;
    status:number;
    OrderDetails: string;
}