import DesktopPage from './(desktop)';
import AdaptiveLayout from '../../layouts/adaptive-layout';

export default function Chat() {
    return <AdaptiveLayout MobilePage={DesktopPage} DesktopPage={DesktopPage} />;
}