[<RequireQualifiedAccess>]
module RangeSelectionView
open System
open Feliz
open Feliz.AgGrid
open AppTypes

[<ReactComponent>]
let BtnCellRenderer props =
  Html.button [
    prop.classes [| "btn"; "btn-primary"|]
    prop.text "button text"
  ]

[<ReactComponent>]
let RenderAgGrid (state: AppState) dispatch =
  Html.div [
    prop.classes [ ThemeClass.Balham; "ag-grid-container" ]
    prop.children [
      AgGrid.grid [
        AgGrid.onCellValueChanged (fun r -> r |> DoStateChanges |> dispatch)
        AgGrid.onGridReady (fun _ -> ()
          //Browser.Dom.console.log ("Grid Ready")
          //Browser.Dom.console.log  state.GridData
        )

        //AgGrid.reactUi               true
        AgGrid.immutableData         true
        AgGrid.getRowNodeId           (fun (r: Rifle) -> r.Id)
        AgGrid.modules               [| clientSideRowModelModule; allEnterpriseModules; rangeSelectionModule  |]
        AgGrid.rowData               state.GridData

        // https://www.ag-grid.com/react-grid/range-selection-fill-handle/
        AgGrid.rowSelection          Multiple
        AgGrid.enableRangeSelection  true
        AgGrid.enableFillHandle      true
        AgGrid.enableRangeHandle     true

        AgGrid.columnDefs [
          ColumnDef.create<Guid> [
            ColumnDef.headerName  "ID"
            ColumnDef.field       "Id"
            ColumnDef.editable    (fun _ -> true)
            ColumnDef.valueGetter (fun d -> d.Id)
          ]

          ColumnDef.create<string> [
            ColumnDef.headerName  "Name"
            ColumnDef.field       "Name"
            ColumnDef.editable    (fun _ -> true)
            ColumnDef.valueGetter (fun d -> d.Name)
          ]

          ColumnDef.create<int> [
            ColumnDef.headerName  "Ammount"
            ColumnDef.field       "Ammount"
            ColumnDef.editable    (fun _ -> true)
            ColumnDef.valueGetter (fun d -> d.Ammount)
          ]
  
          ColumnDef.create<int option> [
            ColumnDef.field      "AmmoInMagazine"
            ColumnDef.headerName "AmmoInMagazine"
            ColumnDef.valueGetter (fun d -> d.AmmoInMagazine)
          ]

          ColumnDef.create<decimal> [
            ColumnDef.headerName "Caliber"
            ColumnDef.valueGetter (fun d -> d.Caliber)
          ]

          ColumnDef.create<DateTime> [
            ColumnDef.headerName "DateServiced"
            ColumnDef.valueGetter (fun d -> d.DateServiced)
          ]
        ]
      ]
    ]
  ]