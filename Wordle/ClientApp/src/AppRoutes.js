import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import Game from './components/Game';
import Profile from './components/Profile';
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
  {
    path: "/profile",
    element: <Profile/>
  },

  ...ApiAuthorzationRoutes
];

export default AppRoutes;
