<?php

namespace App\Http\Controllers;
use Illuminate\Http\Request;
use DB;
use App\Http\Requests;
use App\Http\Controllers\Controller;
class Prodcontroller extends Controller {

public function index(){
$production = DB::select('select * from production');
return view('production',['production'=>$production]);
}
public function destroy($id) {
    DB::delete('delete from production where id = ?',[$id]);
    return redirect('production');
    }
public function search(Request $request){;
        $search = $request->get('search');
        $production = DB::select('select * from production WHERE pcb_id=?',[$search]);
        return view('production',['production'=>$production]);
}
}