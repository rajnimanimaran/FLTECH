export class QC_tMaster{
    QCID: number;
    QCCode: string;
    QCName: string;
    ChkListID: number;
    PrdID: number;
   // prdName: string;
   QCDetails: string;

}

export class DropPName
{
    prdID: number;
    prdName: string;


}
export class AddRow
{
    ChkName: string;
    ChkListID: number;
}
export class Getmodel{
    
    QCID: number;
    ChkListID: number;
}

