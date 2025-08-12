//webpack.config.js
const path = require('path');

module.exports = {
    mode: "development",
    // mode: "production",
    devtool: "inline-source-map",
    entry: {
        main: "./src/InfiniLore.InfiniBlazor/TypescriptLib/Index.ts",
    },
    output: {
        path: path.resolve(__dirname, './src/InfiniLore.InfiniBlazor/wwwroot'),
        filename: "InfiniBlazor.js" // <--- Will be compiled to this single file
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