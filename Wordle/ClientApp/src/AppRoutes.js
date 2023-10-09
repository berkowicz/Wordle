import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import Game from './components/Game';
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: "/game",
    element: <Game/>
  },

  ...ApiAuthorzationRoutes
];

export default AppRoutes;
