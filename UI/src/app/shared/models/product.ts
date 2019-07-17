export class Product {
    prdID: number;
    prdName: string;
    prdCode: string;
    UOMID: number;
    UOMName:string;
    splitable: boolean= false;
    stock: number;
    re_Orderlevel: number;
    HSNCode: string;
    sell_price: number;
    cost_price: number;
    IsActive: boolean
}

export class UOMMaster {
    UOMID: number;
    UOMName: string;
}

export class ProductMaster{
    prdID:number;
    prdName:string;
}
