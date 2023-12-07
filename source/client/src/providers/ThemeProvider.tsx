//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { FluentProvider, Theme } from '@fluentui/react-components';

interface ThemeContextState {
  theme: Theme
}

const ThemeContext = React.createContext<ThemeContextState | undefined>(undefined);

export const useTheme = (): ThemeContextState => {
  const value = React.useContext(ThemeContext);
  if (value == null) {
    throw new Error();
  }
  return value;
};

interface ThemeProviderProps {
  theme: Theme
}

function ThemeProvider(props: Readonly<React.PropsWithChildren<ThemeProviderProps>>) {

  const {
    children,
    theme
  } = props;

  const state = React.useMemo(() => ({
    theme
  }), [
    theme
  ]);

  return (
    <ThemeContext.Provider value={state}>
      <FluentProvider theme={state.theme}>
        {children}
      </FluentProvider>
    </ThemeContext.Provider>
  );

}

export default ThemeProvider;
