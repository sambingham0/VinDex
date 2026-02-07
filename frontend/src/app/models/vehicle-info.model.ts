import { EngineInfo } from './engine-info.model';
import { ManufacturingInfo } from './manufacturing-info.model';

export interface VehicleInfoDto {
    // Basic identity (required)
    vin: string;
    make: string;
    model: string;
    year: number;

    // Optional identity/details
    trim?: string;
    series?: string;
    bodyStyle?: string;
    vehicleType?: string;
    doors?: string;

    // Engine & manufacturing
    engine?: EngineInfo;
    manufacturing?: ManufacturingInfo;

    // Safety & restraints
    abs?: string;
    esc?: string;
    airBagFront?: string;
    airBagSide?: string;
    airBagCurtain?: string;
    laneKeepSystem?: string;
    blindSpotMonitoring?: string;
    tpms?: string;
    daytimeRunningLights?: string;

    // Wheels / dimensions
    wheelBase?: string;
    wheelSizeFront?: string;
    wheelSizeRear?: string;
    curbWeight?: string;
    topSpeedMph?: string;

    // Options / features
    keylessIgnition?: string;
    adaptiveCruiseControl?: string;
    laneDepartureWarning?: string;
    parkAssist?: string;
    automaticPedestrianAlertingSound?: string;
    blindSpotIntervention?: string;

    // Errors / status
    errorCode?: string;
    errorText?: string;
}
