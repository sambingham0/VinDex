export interface RecallDto {
    manufacturer: string;
    campaignNumber: string;
    component: string;
    summary: string;
    consequence: string;
    remedy: string;
    notes: string;
    reportReceivedDate: Date | null;
}
