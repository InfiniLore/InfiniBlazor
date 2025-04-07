//webpack.config.js
const path = require('path');

module.exports = {
    mode: "development",
    // mode: "production",
    devtool: "inline-source-map",
    entry: {
        main: "./src/InfiniLore.Blazor/TsLib/WindowHelper.ts",
    },
    output: {
        path: path.resolve(__dirname, './src/InfiniLore.Blazor/wwwroot'),
        filename: "InfiniLoreBlazor.js" // <--- Will be compiled to this single file
    },
    resolve: {
        extensions: [".ts", ".tsx", ".js"],
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                loader: "ts-loader",
                exclude: /node_modules/
            }
        ]
    }
};