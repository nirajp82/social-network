import React, { SyntheticEvent } from 'react';
import { Modal } from 'semantic-ui-react';

export enum modalSize {
    Mini = 'mini',
    Tiny = 'tiny',
    Small = 'small',
    Large = 'large',
    Fullscreen = 'fullscreen'
};

interface IProps {
    content: any;
    actions?: any;
    trigger?: any;

    closeIcon?: boolean;
    closeOnEscape?: boolean;
    closeOnDocumentClick?: boolean;
    closeOnDimmerClick?: boolean;
    defaultOpen?: boolean;
    //
    open?: boolean,
    size?: modalSize,
    title?: string;

    onClose?: (event: SyntheticEvent, data: object) => void;
    onOpen?: (event: SyntheticEvent, data: object) => void;
    onActionClick?: (event: SyntheticEvent, data: object) => void;
};

const ModelContainer: React.FC<IProps> = (props) => {
    const settings: IProps = {
        trigger: props.trigger,
        content: props.content,
        title: props.title,
        actions: props.actions,
        closeIcon: props.closeIcon || true,
        closeOnDocumentClick: props.closeOnDocumentClick || false,
        closeOnDimmerClick: props.closeOnDocumentClick || false,
        closeOnEscape: props.closeOnEscape || false,
        open: props.open,
        size: props.size || modalSize.Large,
        defaultOpen: props.defaultOpen || false,
        onClose: props.onClose,
        onOpen: props.onOpen,
        onActionClick: props.onActionClick
    };

    const onOpen = (event: SyntheticEvent, data: object) => {
        if (settings.onOpen) {
            settings.onOpen(event, data);
        }
    };

    const onClose = (event: SyntheticEvent, data: object) => {
        event.preventDefault();
        if (settings.onClose) {
            settings.onClose(event, data);
        }
    };

    const onActionClick = (event: SyntheticEvent, data: object) => {
        if (settings.onActionClick) {
            settings.onActionClick(event, data);
        }
    };

    return (
        <Modal
            closeIcon={settings.closeIcon}
            defaultOpen={settings.defaultOpen}
            closeOnDimmerClick={settings.closeOnDimmerClick}
            closeOnDocumentClick={settings.closeOnDocumentClick}
            closeOnEscape={settings.closeOnEscape}
            size={settings.size as any}
            open={settings.open}
            onOpen={onOpen}
            onClose={onClose}
            onActionClick={onActionClick}
        >

            {settings.title && (<Modal.Header>{settings.title}</Modal.Header>)}

            <Modal.Content>
                {settings.content}
            </Modal.Content>

            {settings.actions && (<Modal.Actions>{settings.actions}</Modal.Actions>)}
        </Modal>
    );
};

export default ModelContainer;