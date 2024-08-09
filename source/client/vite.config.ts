//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import fs from 'fs';

import reactPlugin from '@vitejs/plugin-react';
import { defineConfig } from 'vite';
import envPlugin from 'vite-plugin-env-compatible';

export default defineConfig({
  plugins: [
    reactPlugin({
      jsxImportSource: '@emotion/react'
    }),
    envPlugin({
      prefix: 'VITE'
    })
  ],
  server: {
    https: {
      cert: fs.readFileSync('./cert/localhost.crt'),
      key: fs.readFileSync('./cert/localhost.key')
    }
  }
});
