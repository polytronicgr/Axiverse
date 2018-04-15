import * as React from 'react';
import classNames from 'classnames';

import styles from './button.css';

export interface Props {
    value?: string;

    className?: any;
}

class Button extends React.Component<Props> {
    public static defaultProps: Partial<Props> = {
        
    };

    public render(): React.ReactNode {
        let className = classNames(styles.button, this.props.className);
        return (
            <button className={className}>
                {this.props.value}
                {this.props.children}
            </button>
        );
    }
}

export default Button;