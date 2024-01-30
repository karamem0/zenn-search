//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import reactPlugin from '@vitejs/plugin-react';
import { defineConfig } from 'vite';
import envPlugin from 'vite-plugin-env-compatible';

export default defineConfig({
  plugins: [
    envPlugin({
      prefix: 'VITE'
    }),
    reactPlugin({
      jsxImportSource: '@emotion/react'
    })
  ]
});
