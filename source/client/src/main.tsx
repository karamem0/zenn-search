//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import ReactDOM from 'react-dom/client';

import { webLightTheme } from '@fluentui/react-components';

import MainPage from './pages/MainPage';
import TelemetryProvider from './providers/TelemetryProvider';
import ThemeProvider from './providers/ThemeProvider';

import 'ress';

ReactDOM
  .createRoot(document.getElementById('root') as Element)
  .render(
    <React.StrictMode>
      <TelemetryProvider>
        <ThemeProvider theme={webLightTheme}>
          <MainPage />
        </ThemeProvider>
      </TelemetryProvider>
    </React.StrictMode>
  );
