module AppTypes
open System

[<RequireQualifiedAccess>]
type Route =
  | Home
  | Simple
  | RangeSelection
  | CellRenderer
  | Invalid

type Rifle = {
  Id             : Guid
  Name           : string
  Caliber        : decimal
  Ammount        : int
  DateServiced   : DateTime
  AmmoInMagazine : int option 
  }

type AppState = { 
  CurrentRoute   : Route
  GridData       : Rifle array
  }
  with
  static member Empty = {
    CurrentRoute = Route.Home
    GridData     = [||]
  }

type Msg =
  | CreateGridData
  | NavigateHome
  | NavigateSimple
  | NavigateRangeSelection
  | NavigateCellRenderer
  | EraseGriData
  | RifleChanged of Rifle