import DesktopPage from './(desktop)';
import MobilePage from './(mobile)';
import AdaptiveLayout from '../../layouts/adaptive-layout';

export default function AppDetail() {

    return <AdaptiveLayout MobilePage={MobilePage} DesktopPage={DesktopPage} />;
}