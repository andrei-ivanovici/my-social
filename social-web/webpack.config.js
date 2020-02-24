const HtmlWebpackPlugin = require('html-webpack-plugin');
module.exports = {
    output: {
        path: "/widget"
    },
    entry: {
        main: "./src/index.tsx"
    },
    resolve: {
        extensions: [".js", ".jsx", ".tsx", ".ts"]
    },
    module: {

        rules: [
            {
                test: /\.tsx$/,
                use: ['ts-loader'],
                exclude: /node_modules/
            },
            {
                test: /(.jsx|.js)$/,
                use: "babel-loader"
            },
            {
                test: /module.css$/,
                use: [
                    "style-loader",
                    {
                        loader: "css-loader",
                        options: {
                            modules: {
                                mode: 'local',
                                localIdentName: '[name]_[local]_[hash:base64:5]_',
                            }
                        }
                    }
                ],
                include: /\.module\.css$/
            },
            {
                test: /\.css$/,
                use: [
                    "style-loader",
                    {
                        loader: "css-loader"
                    }
                ],
                exclude: /\.module\.css$/
            },
        ]
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: "./public/index.html"
        })
    ]
};
