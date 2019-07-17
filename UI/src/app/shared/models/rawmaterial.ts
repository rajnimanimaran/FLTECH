export class RawMaterial {
    RMID: number;
    RMCode: string;
    RMName: string;
    UOMID: number;
    UOMName:string;
    splitable: boolean= false;
    stock: number;
    re_Orderlevel: number;
    sell_price: number;
    cost_price: number;
    IsActive: boolean;
}
export class rmName{
    RMID: number;
    RMName: string;
}