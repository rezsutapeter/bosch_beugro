<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- Scripts -->
        <script src="{{ asset('js/app.js') }}" defer></script>
        <!-- Styles -->
        <link href="{{ asset('css/app.css') }}" rel="stylesheet">
        <title>Beugro</title>
    </head>
    <body>
        <div id="app">
        @include('layouts.nav')
            <main class="py-4 main">
                @yield('content')
            </main>
        </div>
    </body>
</html>
