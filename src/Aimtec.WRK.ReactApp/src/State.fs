[<RequireQualifiedAccess>]
module AppState
open System
open AppTypes
open Elmish

let init () = AppState.Empty, CreateGridData |> Cmd.ofMsg

let update (msg: AppTypes.Msg) (state: AppTypes.AppState) =
  match msg with
  | CreateGridData -> 
    let rifles = [|
      {Id=Guid.NewGuid(); Name = "Ak-74N"; Caliber=5.45m;Ammount=1;DateServiced=DateTime.Now;AmmoInMagazine=None}
      {Id=Guid.NewGuid(); Name = "M4A1"; Caliber=5.56m;Ammount=4;DateServiced=DateTime.Now;AmmoInMagazine=Some 30}
      {Id=Guid.NewGuid(); Name = "Spas-15"; Caliber=12m;Ammount=5;DateServiced=DateTime.Now;AmmoInMagazine=None}
      {Id=Guid.NewGuid(); Name = "AN-94"; Caliber=5.45m;Ammount=1;DateServiced=DateTime.Now;AmmoInMagazine=None}
      {Id=Guid.NewGuid(); Name = "AK-115"; Caliber=5.45m;Ammount=1;DateServiced=DateTime.Now;AmmoInMagazine=None}
      {Id=Guid.NewGuid(); Name = "AKM"; Caliber=7.62m;Ammount=1;DateServiced=DateTime.Now;AmmoInMagazine=None}
      {Id=Guid.NewGuid(); Name = "Dragunov"; Caliber=7.62m;Ammount=1;DateServiced=DateTime.Now;AmmoInMagazine=None}
      {Id=Guid.NewGuid(); Name = "MP5"; Caliber=5.45m;Ammount=1;DateServiced=DateTime.Now;AmmoInMagazine=None}
    |]

    {state with GridData = rifles}, Cmd.none
  | EraseGriData -> 
    state, Cmd.none