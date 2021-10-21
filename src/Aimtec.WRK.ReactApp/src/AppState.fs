[<RequireQualifiedAccess>]
module AppState
open AppTypes
open AppRouter
open System
open AppTypes
open Elmish
open Elmish.Navigation

let init result =
  let state, cmd = urlUpdate result AppState.Empty
  state, Cmd.batch [
    CreateGridData |> Cmd.ofMsg
    cmd
  ]

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

  | NavigateHome ->
    state, ("#") |> Navigation.newUrl

  | NavigateSimple ->
    state, ("#/Simple") |> Navigation.newUrl

  | NavigateRangeSelection ->
    state, ("#/RangeSelection") |> Navigation.newUrl

  | NavigateCellRenderer ->
    state, ("#/CellRenderer") |> Navigation.newUrl

  | EraseGriData -> 
    state, Cmd.none