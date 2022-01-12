// fsharplint:disable
module Feliz.AgGrid
open Fable.Core
open Fable.Core.JsInterop
open Feliz

let agGrid                   : obj = import "AgGridReact"              "@ag-grid-community/react"
let clientSideRowModelModule : obj = import "ClientSideRowModelModule" "@ag-grid-community/client-side-row-model"
let allEnterpriseModules     : obj = import "AllModules"               "@ag-grid-enterprise/all-modules"
let licenceManager           : obj = import "LicenseManager"           "@ag-grid-enterprise/all-modules"
let rangeSelectionModule     : obj = import "RangeSelectionModule"     "@ag-grid-enterprise/range-selection"
let clipboardModule          : obj = import "ClipboardModule"          "@ag-grid-enterprise/clipboard"

importAll "@ag-grid-community/core/dist/styles/ag-grid.css"
importAll "@ag-grid-community/core/dist/styles/ag-theme-alpine.css"
importAll "@ag-grid-community/core/dist/styles/ag-theme-alpine-dark.css"
importAll "@ag-grid-community/core/dist/styles/ag-theme-balham.css"
importAll "@ag-grid-community/core/dist/styles/ag-theme-balham-dark.css"
importAll "@ag-grid-community/core/dist/styles/ag-theme-material.css"

type RowSelection = Single | Multiple
type RowFilter = Number | Text | Date member this.FilterText = sprintf "ag%OColumnFilter" this
type DOMLayout =
  Normal | AutoHeight | Print
  member this.LayoutText =
    match this with
    | Normal     -> "normal"
    | AutoHeight -> "autoHeight"
    | Print      -> "print"

module ThemeClass =
  let Alpine     = "ag-theme-alpine"
  let AlpineDark = "ag-theme-alpine-dark"
  let Balham     = "ag-theme-balham"
  let BalhamDark = "ag-theme-balham-dark"
  let Material   = "ag-theme-material"

[<Erase>]
type IColumnDefProp<'row, 'value> = interface end
let columnDefProp<'row, 'value> = unbox<IColumnDefProp<'row, 'value>>

[<Erase>]
type IColumnDef<'row> = interface end
type ColumnType = RightAligned | NumericColumn

let openClosed = function | true -> "open" | false -> "closed"

[<ReactComponent>]
let CellRendererComponent<'value,'row> (render:'value -> 'row -> ReactElement, p) =
  render p?value p?data

[<Erase>]
type ColumnDef<'row, 'value> =
  static member inline headerTooltip (v: string) = 
    columnDefProp<'row, 'value> ("headerTooltip" ==> v)

  static member inline field (v: string) = 
    columnDefProp<'row, 'value> ("field" ==> v)

  static member inline cellEditor (v: string) = 
    columnDefProp<'row, 'value> ("cellEditor" ==> v)

  static member inline cellEditorParams (v: obj) =
    columnDefProp<'row, 'value> ("cellEditorParams" ==> v)

  static member inline cellRenderer (v: string) =
    columnDefProp<'row, 'value> ("cellRenderer" ==> v)

  static member inline cellRendererParams (v: obj) = 
    columnDefProp<'row, 'value> ("cellRendererParams" ==> v)

  static member inline resizable (v:bool) = 
    columnDefProp<'row, 'value> ("resizable" ==> v)

  static member inline editable (f:'row -> bool) = 
    columnDefProp<'row, 'value> ("editable" ==> (fun p -> f p?data))

  static member inline filter (v:RowFilter) = 
    columnDefProp<'row, 'value> ("filter" ==> v.FilterText)

  static member inline sortable (v:bool) =
    columnDefProp<'row, 'value> ("sortable" ==> v)

  static member inline valueGetter (f:'row -> 'value) =
    columnDefProp<'row, 'value> ("valueGetter" ==> (fun x -> f x?data))

  static member inline valueSetter (f:string -> 'value -> 'row -> unit) =
    columnDefProp<'row, 'value> ("valueSetter" ==> (fun x ->
    f x?oldValue x?newValue x?data
    // return false to prevent the grid from immediately refreshing
    false))

  static member inline valueParser (f: 'value -> 'value -> 'row -> 'value) =
    columnDefProp<'row, 'value> ("valueParser" ==> (fun p ->
      f p?oldValue p?newValue p?data))
  
  static member inline valueFormatter (v:'value -> 'row -> string) =
    columnDefProp<'row, 'value> ("valueFormatter" ==> (fun p -> v p?value p?data))

  static member cellRendererFramework (render:'value -> 'row -> ReactElement) = 
    columnDefProp<'row, 'value> ("cellRendererFramework" ==> fun p -> CellRendererComponent(render, p))

  static member inline width (v:int) = 
    columnDefProp<'row, 'value> ("width" ==> v)

  static member inline minWidth (v:int) = 
    columnDefProp<'row, 'value> ("minWidth" ==> v)

  static member inline maxWidth (v:int) = 
    columnDefProp<'row, 'value> ("maxWidth" ==> v)

  static member inline headerCheckboxSelection (v:bool) =
    columnDefProp<'row, 'value> ("headerCheckboxSelection" ==> v)

  static member inline checkboxSelection (v:bool) = 
    columnDefProp<'row, 'value> ("checkboxSelection" ==> v)

  static member inline headerName (v:string) = 
    columnDefProp<'row, 'value> ("headerName" ==> v)

  static member inline hide (v:bool) = 
    columnDefProp<'row, 'value> ("hide" ==> v)

  static member inline onCellClicked (handler:'value -> 'row -> unit) =
    columnDefProp<'row, 'value> ("onCellClicked" ==> (fun p -> handler p?value p?data))

  static member inline cellStyle (setStyle:'value -> 'row -> _) =
    columnDefProp<'row, 'value> ("cellStyle" ==> fun p -> setStyle p?value p?data)

  static member inline cellClass (setClass:'value -> 'row -> #seq<string>) =
    columnDefProp<'row, 'value> ("cellClass" ==> fun p -> setClass p?value p?data |> Seq.toArray)

  static member inline cellClassRules (rules: (string*('value -> 'row -> bool)) list) =
    columnDefProp<'row, 'value> ("cellClassRules" ==> (rules |> List.map (fun (className, rule) -> className ==> fun p -> rule p?value p?data)|> createObj))

  static member inline columnType ct = 
    columnDefProp<'row, 'value> ("type" ==> match ct with RightAligned -> "rightAligned" | NumericColumn -> "numericColumn")

  static member inline comparator (callback: 'a -> 'a -> int) =
    columnDefProp<'row, 'value> ("comparator" ==> fun a b -> callback a b)

  static member inline autoComparator =
    columnDefProp<'row, 'value> ("comparator" ==> compare)

  static member inline columnGroupShow (v:bool) = 
    columnDefProp<'row, 'value> ("columnGroupShow" ==> openClosed v)

  static member inline create<'v> (props:seq<IColumnDefProp<'row, 'v>>) = props |> unbox<_ seq> |> createObj |> unbox<IColumnDef<'row>>


[<Erase>]
type IColumnGroupDefProp<'row> = interface end
let columnGroupDefProp<'row> = unbox<IColumnGroupDefProp<'row>>

[<Erase>]
type ColumnGroup<'row> =
  static member inline headerName (v:string) = 
    columnGroupDefProp<'row> ("headerName" ==> v)

  static member inline marryChildren(v:bool) = 
    columnGroupDefProp<'row> ("marryChildren" ==> v)
  
  static member inline openByDefault(v:bool) =
    columnGroupDefProp<'row> ("openByDefault" ==> v)

  static member inline create<'row> (props:seq<IColumnGroupDefProp<'row>>) (children:seq<IColumnDef<'row>>) =
    props 
    |> Seq.append [(columnGroupDefProp<'row> ("children" ==> Seq.toArray children))]
    |> unbox<_ seq> 
    |> createObj 
    |> unbox<IColumnDef<'row>>


[<Erase>]
type IAgGridProp<'row> = interface end
let agGridProp<'row> (x:obj) = unbox<IAgGridProp<'row>> x


[<Erase>]
type AgGrid() =
  // --- Clipboard: https://ag-grid.com/react-data-grid/clipboard/#clipboard-events

  /// Paste event has started.
  static member inline onPasteStart callback =
    agGridProp<'row>("onPasteStart", callback)

  /// Paste event has ended.
  static member inline onPasteEnd callback =
    agGridProp<'row>("onPasteEnd", callback)

  /// Allows you to process cells from the clipboard. Handy if for example you have number fields, and want to block non-numbers from getting into the grid.
  static member inline processCellForClipboard callback =
    agGridProp<'row>("processCellForClipboard", callback)

  /// Allows you to process cells from the clipboard. Handy if for example you have number fields, and want to block non-numbers from getting into the grid.
  static member inline processCellFromClipboard callback =
    agGridProp<'row>("processCellFromClipboard", callback)
  
  /// Allows complete control of the paste operation, including cancelling the operation (so nothing happens) or replacing the data with other data.
  static member inline processDataFromClipboard callback =
    agGridProp<'row>("processDataFromClipboard", callback)


  static member inline rowClassRules (rules: (string*('row -> bool)) list) =
    agGridProp<'row>("rowClassRules" ==> (rules |> List.map (fun (className, rule) -> className ==> fun p -> rule p?data)|> createObj))

  static member inline frameworkComponents (v:obj) =
    agGridProp<'row>("frameworkComponents", v)

  static member inline components (v:obj) = 
    agGridProp<'row>("components", v)

  static member inline gridOptions (v:obj) =
    agGridProp<'row>("gridOptions", v)

  static member inline modules (modules:obj array) = 
    agGridProp<'row>("modules", modules)

  static member inline reactUi (v:bool) = 
    agGridProp<'row>("reactUi" ==> v)

  static member inline className (v:string) =
    agGridProp<'row>("className" ==> v)

  static member inline enableCellChangeFlash (v:bool) =
    agGridProp<'row>("enableCellChangeFlash" ==> v)

  static member inline cellFlashDelay (v:int) =
    agGridProp<'row>("cellFlashDelay" ==> v)

  static member inline animateRows (v:bool) = 
    agGridProp<'row>("animateRows" ==> v)

  static member inline enableRangeSelection (v:bool) = 
    agGridProp<'row>("enableRangeSelection", v)

  static member inline enableFillHandle (v:bool) = 
    agGridProp<'row>("enableFillHandle" ==> v)

  static member inline enableRangeHandle (v:bool) = 
    agGridProp<'row>("enableRangeHandle" ==> v)

  static member inline onSelectionChanged (callback:'row array -> unit) =
    agGridProp<'row>("onSelectionChanged", fun x -> x?api?getSelectedRows() |> callback)

  static member inline onCellValueChanged callback = 
    agGridProp<'row>("onCellValueChanged", fun x -> callback x?data)

  static member inline onCellEditingStopped callback = 
    agGridProp<'row>("onCellEditingStopped", fun x -> callback x?node?lastChild x?data)

  static member inline onRowClicked (handler:'value -> 'row -> unit) =
    agGridProp<'row> ("onRowClicked" ==> (fun p -> handler p?value p?data))

  static member inline singleClickEdit (v:bool) = 
    agGridProp<'row>("singleClickEdit" ==> v)

  static member inline rowDeselection (v:bool) = 
    agGridProp<'row>("rowDeselection", v)

  static member inline rowSelection (s:RowSelection) =
    agGridProp<'row>("rowSelection", s.ToString().ToLower())

  static member inline isRowSelectable (callback:'row -> bool) =
    agGridProp<'row>("isRowSelectable" ==> fun x -> x?data |> callback)

  static member inline suppressRowClickSelection (v:bool) = 
    agGridProp<'row>("suppressRowClickSelection" ==> v)

  static member inline rowHeight (h:int) = 
    agGridProp<'row>("rowHeight", h)

  static member inline domLayout (l:DOMLayout) =
    agGridProp<'row>("domLayout", l.LayoutText)

  static member inline immutableData (v:bool) = 
    agGridProp<'row>("immutableData", v)

  static member inline rowData (data:'row array) = 
    agGridProp<'row>("rowData", data)

  static member inline getRowNodeId (callback: 'row -> _) =
    agGridProp<'row>("getRowNodeId", callback)

  static member inline columnDefs (columns:IColumnDef<'row> seq) =
    agGridProp<'row>("columnDefs", columns |> unbox |> Seq.toArray)

  static member inline defaultColDef (defaults:IColumnDefProp<'row, 'value> seq) =
    agGridProp<'row>("defaultColDef", defaults |> unbox<_ seq> |> createObj)

  static member onColumnGroupOpened (callback:_ -> unit) = // This can't be inline otherwise Fable produces invalid JS
      let onColumnGroupOpened = fun ev ->
          {| AutoSizeGroupColumns = fun () ->
              // Runs the column autoSize in a 0ms timeout so that the cellRendererFramework cells render
              // before the grid calculates how large each cell is
              JS.setTimeout (fun () ->
                  let colIds = ev?columnGroup?children |> Array.map (fun x -> x?colId)
                  ev?columnApi?autoSizeColumns(colIds)) 0 |> ignore |}
          |> callback
      agGridProp<'row>("onColumnGroupOpened", onColumnGroupOpened)

  static member inline paginationPageSize (pageSize:int) = 
    agGridProp<'row>("paginationPageSize", pageSize)

  static member inline paginationAutoPageSize (v:bool) =
    agGridProp<'row>("paginationAutoPageSize", v)

  static member inline pagination (v:bool) =
    agGridProp<'row>("pagination", v)

  static member inline onGridReady callback = 
    agGridProp<'row>("onGridReady", fun x -> callback x?api)

  //static member onGridReady (callback:_ -> unit) = // This can't be inline otherwise Fable produces invalid JS
  //    let onGridReady = fun ev ->
  //        {| AutoSizeAllColumns =
  //            fun () ->
  //                // Runs the column autoSize in a 0ms timeout so that the cellRendererFramework cells render
  //                // before the grid calculates how large each cell is
  //                JS.setTimeout (fun () ->
  //                    let colIds = ev?columnApi?getAllColumns() |> Array.map (fun x -> x?colId)
  //                    ev?columnApi?autoSizeColumns(colIds)) 0 |> ignore
  //           Export = fun () -> ev?api?exportDataAsCsv(obj()) |}
  //        |> callback
  //    agGridProp<'row>("onGridReady", onGridReady)
  
  static member inline key (v:string) = 
    agGridProp<'row> (prop.key v)

  static member inline key (v:int) =
    agGridProp<'row> (prop.key v)

  static member inline key (v:System.Guid) =
    agGridProp<'row> (prop.key v)

  static member inline grid<'row> (props:IAgGridProp<'row> seq) =
    Interop.reactApi.createElement (agGrid, createObj !!props)

[<Import("LicenseManager", from="@ag-grid-enterprise/all-modules")>]
type LicenceManager () =
  static member inline setLicenseKey (k:string) = jsNative