module AppTypes
open System

type Rifle = {
  Id             : Guid
  Name           : string
  Caliber        : decimal
  Ammount        : int
  DateServiced   : DateTime
  AmmoInMagazine : int option 
  }

type AppState = { 
  GridData       : Rifle array
  }
  with
  static member Empty = {
    GridData     = [||]
  }

type Msg =
  | CreateGridData
  | EraseGriData