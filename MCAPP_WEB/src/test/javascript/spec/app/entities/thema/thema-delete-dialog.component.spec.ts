/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { JhiEventManager } from 'ng-jhipster';

import { McappWebTestModule } from '../../../test.module';
import { ThemaDeleteDialogComponent } from 'app/entities/thema/thema-delete-dialog.component';
import { ThemaService } from 'app/entities/thema/thema.service';

describe('Component Tests', () => {
    describe('Thema Management Delete Component', () => {
        let comp: ThemaDeleteDialogComponent;
        let fixture: ComponentFixture<ThemaDeleteDialogComponent>;
        let service: ThemaService;
        let mockEventManager: any;
        let mockActiveModal: any;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [ThemaDeleteDialogComponent]
            })
                .overrideTemplate(ThemaDeleteDialogComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(ThemaDeleteDialogComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(ThemaService);
            mockEventManager = fixture.debugElement.injector.get(JhiEventManager);
            mockActiveModal = fixture.debugElement.injector.get(NgbActiveModal);
        });

        describe('confirmDelete', () => {
            it('Should call delete service on confirmDelete', inject(
                [],
                fakeAsync(() => {
                    // GIVEN
                    spyOn(service, 'delete').and.returnValue(of({}));

                    // WHEN
                    comp.confirmDelete(123);
                    tick();

                    // THEN
                    expect(service.delete).toHaveBeenCalledWith(123);
                    expect(mockActiveModal.dismissSpy).toHaveBeenCalled();
                    expect(mockEventManager.broadcastSpy).toHaveBeenCalled();
                })
            ));
        });
    });
});
